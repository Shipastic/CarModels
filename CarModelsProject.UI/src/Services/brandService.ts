import {Brand} from "../Models/Dto";

const API_URL = "https://localhost:7156/api/Brands";

export const getBrands = async (): Promise<Brand[]> => {
    const response = await fetch(`${API_URL}`);
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data: Brand[] = await response.json();
    return data;
};
