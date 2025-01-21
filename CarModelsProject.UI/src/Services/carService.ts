import {Car, CarCreateUpdateDto} from "../Models/Dto";

interface PagedResponse<T> {
    data: T[];
    totalItems: number;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
}
const API_URL = "https://localhost:7156/api/Cars";

export const getCars = async (pageNumber: number, pageSize: number): Promise<PagedResponse<Car>> => {
    const response = await fetch(`${API_URL}?PageNumber=${pageNumber}&PageSize=${pageSize}`);
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data: PagedResponse<Car> = await response.json();
    return data;
};

export const getCar = async (id: number): Promise<Car> => {
    const response = await fetch(`${API_URL}/${id}`);
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data: Car = await response.json();
    return data;
};

export const createCar = async (carData: CarCreateUpdateDto): Promise<CarCreateUpdateDto> => {
    const response = await fetch(API_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(carData)
    });
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data: CarCreateUpdateDto = await response.json();
    return data;
};

export const updateCar = async (id: number, carData: CarCreateUpdateDto): Promise<void> => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(carData)
    });
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
};

export const deleteCar = async (id: number): Promise<void> => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: 'DELETE'
    });
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
};