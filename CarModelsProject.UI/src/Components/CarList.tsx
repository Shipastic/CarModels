import React, { useEffect, useState } from "react";
import { Table, Pagination,Button} from 'rsuite';
import { getCars, deleteCar, createCar, updateCar } from "../Services/carService";
import {getBodyStyles} from "../Services/bodyStyleService";
import {getBrands} from "../Services/brandService";
import { Car, CarCreateUpdateDto, BodyStyle, Brand  } from "../Models/Dto";
import AddEditModalWindow from "../Modals/AddEditModalWindow"; 
import '../Components/CarList.css';

const { Column, HeaderCell, Cell } = Table;
const CarList: React.FC = () => {
    const [cars, setCars] = useState<Car[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [activePage, setActivePage] = useState<number>(1);
    const [limit, setLimit] = useState(10);
    const [total, setTotal] = useState(0);
    const [showForm, setShowForm] = useState<boolean>(false);
    const [editMode, setEditMode] = useState<boolean>(false);
    const [currentCarData, setCurrentCarData] = useState<CarCreateUpdateDto>({
        brandId: 0,
        modelName: '',
        bodyStyleId: 0,
        seatsCount: 1,
        dealerUrl: '',
        bodyStyleName:'',
        brandName: '',
        carId: 0
    });
    const [currentCarId, setCurrentCarId] = useState<number | null>(null);
    const [brands, setBrands] = useState<Brand[]>([]);
    const [bodyStyles, setBodyStyles] = useState<BodyStyle[]>([]);

    const fetchCars = async (page: number, pageSize: number) => {
        setLoading(true);
        try {           
            const carsData = await getCars(page, pageSize);
            setCars(carsData.data);
            setTotal(carsData.totalItems);
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    };

    const fetchBrands = async () => {
        try {
            const brandsData = await getBrands();
            setBrands(brandsData);
        } catch (error) {
            console.error(error);
        }
    };

    const fetchBodyStyles = async () => {
        try {
            const bodyStylesData = await getBodyStyles();
            setBodyStyles(bodyStylesData);
        } catch (error) {
            console.error(error);
        }
    };

    useEffect(() => {
        fetchCars(activePage, limit);
        fetchBrands();
        fetchBodyStyles();
    }, [activePage, limit]);

    const handleAddClick = () => {
        setEditMode(false);
        setCurrentCarData({
            brandId: 0,
            modelName: '',
            bodyStyleId: 0,
            seatsCount: 1,
            dealerUrl: '',
            bodyStyleName:'',
            brandName: '',
            carId: 0
        });
        setShowForm(true);
    };

    const handleEditClick = (car: Car) => {
        setEditMode(true);
        setCurrentCarId(car.carId);
        setCurrentCarData({
            brandId: car.brandId,
            modelName: car.modelName,
            bodyStyleId: car.bodyStyleId,
            seatsCount: car.seatsCount,
            dealerUrl: car.dealerUrl || '',
            bodyStyleName:car.bodyStyleName || '',
            brandName: car.brandName || '',
            carId: car.carId
        });
        setShowForm(true);
    };

    const handleFormSubmit = async (carData: CarCreateUpdateDto) => {
        if (editMode && currentCarId !== null) {
            // Обновление существующего автомобиля
            try {
                await updateCar(currentCarId, carData);
                alert('Автомобиль успешно обновлён.');
                setShowForm(false);
                fetchCars(activePage, limit);
            } catch (error) {
                console.error(error);
                alert('Ошибка при обновлении автомобиля.');
            }
        } else {
            // Добавление нового автомобиля
            try {
                await createCar(carData);
                alert('Автомобиль успешно добавлен.');
                setShowForm(false);
                fetchCars(activePage, limit);
            } catch (error) {
                console.error(error);
                alert('Ошибка при добавлении автомобиля.');
            }
        }
    };

    const handleDelete = async (id: number) => {
           if (window.confirm('Вы уверены, что хотите удалить этот автомобиль?')) 
           {
               try 
               {
                   await deleteCar(id);
                   alert('Автомобиль успешно удалён.');
                   
                   fetchCars(activePage, limit);
               } 
               catch (error) 
               {
                   console.error(error);
                   alert('Ошибка при удалении автомобиля.');
               }
           }
       };

    const handlePageChange = (page: number) => {
        console.log('Изменение страницы на:', page);
        setActivePage(page);
    };
    const handleChangeLimit = (newlimit:number) => {
        console.log('Изменение лимита на:', newlimit);
        setLimit(newlimit);
        setActivePage(1);
        
    };
    
       return (
        <div >
            <h1>Список автомобилей</h1>
            <Button onClick={handleAddClick} appearance="primary" style={{ marginBottom: '10px' }}>
                Добавить
            </Button>
            <Table 
                height={500}
                data={cars} 
                loading={loading} 
                onRowClick={(data) => handleEditClick(data)}
                autoHeight
                autoFocus={true}
                autoCorrect="true"
                className='tableStyle'>
                   <Column width={200} align="left" resizable>
                       <HeaderCell>Модель</HeaderCell>
                       <Cell dataKey="modelName"/>
                   </Column>
                   <Column width={200} resizable>
                       <HeaderCell>Бренд</HeaderCell>
                       <Cell dataKey="brandName"/>
                   </Column>
                   <Column width={200} resizable>
                       <HeaderCell>Тип кузова</HeaderCell>
                       <Cell dataKey="bodyStyleName"/>
                   </Column>
                   <Column width={150} resizable>
                       <HeaderCell>Количество мест</HeaderCell>
                       <Cell dataKey="seatsCount"/>
                   </Column>
                   <Column width={200} fixed="right">
                       <HeaderCell>Действия</HeaderCell>
                       <Cell>
                           {(rowData: Car) => (
                               <span>
                                    <Button onClick={() => handleEditClick(rowData)} size="xs" appearance="link">
                                        Редактировать
                                    </Button>
                                   <Button onClick={() => handleDelete(rowData.carId)} color="red" size="xs" appearance="link">
                                       Удалить
                                   </Button>
                               </span>
                           )}
                       </Cell>
                   </Column>
               </Table>
               <Pagination
                   prev
                   next
                   first
                   last
                   ellipsis
                   boundaryLinks
                   maxButtons={5}
                   size="sm"
                   layout={['total', 'limit', '|', 'pager', 'skip']}
                   total={total}
                   limitOptions={[5, 10, 20, 50]}
                   limit={limit}
                   activePage={activePage}
                   onChangePage={handlePageChange}
                   onChangeLimit={handleChangeLimit}
                   style={{ marginTop: '10px', textAlign: 'center' }}
               />
                <AddEditModalWindow
                show={showForm}
                onHide={() => setShowForm(false)}
                onSubmit={handleFormSubmit}
                editMode={editMode}
                initialCarData={currentCarData}
                brands={brands}
                bodyStyles={bodyStyles}
            />
           </div>
       );
   };

   export default CarList;
