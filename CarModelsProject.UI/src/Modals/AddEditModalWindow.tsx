import React, { useState, useEffect } from 'react';
import { Modal, Form, Button, SelectPicker } from 'rsuite';
import { CarCreateUpdateDto, BodyStyle, Brand } from '../Models/Dto';

import './Modal.css';

interface CarFormModalProps {
    show: boolean;
    onHide: () => void;
    onSubmit: (carData: CarCreateUpdateDto) => void;
    editMode: boolean;
    initialCarData: CarCreateUpdateDto;
    brands: Brand[];
    bodyStyles: BodyStyle[];
}
interface FormErrorType {
    brandName?: string;
    modelName?: string;
    bodyStyleName?: string;
    seatsCount?: string;
    dealerUrl?: string;
  }

const AddEditModalWindow: React.FC<CarFormModalProps> = ({ 
                                                            show,
                                                            onHide,
                                                            onSubmit,
                                                            editMode,
                                                            initialCarData,
                                                            brands,
                                                            bodyStyles,}) => {
        const [formValue, setFormValue] = useState<CarCreateUpdateDto>(initialCarData);
        const [errors, setErrors] = useState<FormErrorType>({});

        const validate = () => {
            const newErrors = {};
          
            if (!formValue.brandName) {
              newErrors.brandName = 'Бренд обязателен.';
            }
            if (!formValue.modelName) {
              newErrors.modelName = 'Модель обязательна.';
            }
            if (!formValue.bodyStyleName) {
              newErrors.bodyStyleName = 'Тип кузова обязателен.';
            }
            if (!formValue.seatsCount) {
              newErrors.seatsCount = 'Количество мест обязательно.';
            } else if (formValue.seatsCount < 1 || formValue.seatsCount > 12) {
              newErrors.seatsCount = 'Количество мест должно быть от 1 до 12.';
            }
            if (!formValue.dealerUrl) {
              newErrors.dealerUrl = 'Сайт дилера обязателен.';
            } else {
              // Пример простой проверки URL
              const urlPattern = new RegExp('.*\\.ru$');
              if (!urlPattern.test(formValue.dealerUrl)) {
                newErrors.dealerUrl = 'Введите корректный URL.';
              }
            }
          
            setErrors(newErrors);

            return Object.keys(newErrors).length === 0;
          };

        useEffect(() => {
            setFormValue(initialCarData);
        }, [initialCarData]);
    
        const handleChange = (value: Partial<CarCreateUpdateDto>) => {
            setFormValue((prev) => ({ ...prev, ...value }));
            setErrors({});
        };
    
        const handleSubmit = () => {
            if (validate()) {
            onSubmit(formValue);
            }
        };

        return (
            <Modal open={show} onClose={onHide} className='custom-modal'>
                <Modal.Header>
                    <Modal.Title>{editMode ? 'Редактировать автомобиль' : 'Добавить автомобиль'}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form fluid
                          onChange={handleChange}
                          onCheck={setErrors}>
                        <Form.Group controlId="brandName">
                            <Form.ControlLabel>Бренд</Form.ControlLabel>
                            <Form.Control
                                name="brandName"
                                accepter={SelectPicker}
                                data={brands}
                                value={formValue.brandName}
                                onChange={(value) => handleChange({ brandName: value })}
                                valueKey="name"
                                labelKey="name"
                                searchable={false}
                                cleanable={false}
                                placeholder="Выберите бренд"
                                errorMessage={errors.brandName}
                                errorPlacement="bottomStart"
                            />
                        </Form.Group>
                        <Form.Group controlId="modelName">
                            <Form.ControlLabel>Модель</Form.ControlLabel>
                            <Form.Control
                                name="modelName"
                                value={formValue.modelName}
                                onChange={(value) => handleChange({ modelName: value })}
                                errorMessage={errors.modelName}
                                errorPlacement="bottomStart"
                            />
                        </Form.Group>
                        <Form.Group controlId="bodyStyleName">
                            <Form.ControlLabel>Тип кузова</Form.ControlLabel>
                            <Form.Control
                                name="bodyStyleName"
                                accepter={SelectPicker}
                                data={bodyStyles}
                                value={formValue.bodyStyleName}
                                onChange={(value) => handleChange({ bodyStyleName: value })}
                                valueKey="name"
                                labelKey="name"
                                searchable={false}
                                cleanable={false}
                                placeholder="Выберите тип кузова"
                                errorMessage={errors.bodyStyleName}
                                errorPlacement="bottomStart"
                            />
                        </Form.Group>
                        <Form.Group controlId="seatsCount">
                            <Form.ControlLabel>Количество мест</Form.ControlLabel>
                            <Form.Control
                                name="seatsCount"
                                type="number"
                                min={1}
                                max={12}
                                value={formValue.seatsCount}
                                onChange={(value) => handleChange({ seatsCount: Number(value) })}
                                errorMessage={errors.seatsCount}
                                errorPlacement="bottomStart"
                            />
                        </Form.Group>
                        <Form.Group controlId="dealerUrl">
                            <Form.ControlLabel>Сайт дилера</Form.ControlLabel>
                            <Form.Control
                                name="dealerUrl"
                                value={formValue.dealerUrl}
                                onChange={(value) => handleChange({ dealerUrl: value })}
                                errorMessage={errors.dealerUrl}
                                errorPlacement="bottomStart"
                        />
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button onClick={handleSubmit} appearance="primary">
                    {editMode ? 'Сохранить' : 'Добавить'}
                </Button>
                <Button onClick={onHide} appearance="subtle">
                    Отмена
                </Button>
            </Modal.Footer>
        </Modal>
    );
};
export default AddEditModalWindow;