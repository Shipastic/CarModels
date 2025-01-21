export interface Car {
    carId        : number;
    brandId      : number;
    brandName    : string;
    modelName    : string;
    bodyStyleId  : number;
    bodyStyleName: string;
    seatsCount   : number;
    dealerUrl?   : string;
    createdAt    : string;
}

export interface CarCreateUpdateDto {
    carId        : number;
    brandId      : number;
    brandName    : string;
    modelName    : string;
    bodyStyleId  : number;
    bodyStyleName: string;
    seatsCount   : number;
    dealerUrl?   : string;  
}

export interface Brand {
    id           : number;
    name         : string;
}

export interface BodyStyle {
    id           : number;
    name         : string;
}