import {BodyStyle} from "../Models/Dto";

const API_URL = "https://localhost:7156/api/BodyStyle";

export const getBodyStyles = async (): Promise<BodyStyle[]> => {
    const response = await fetch(`${API_URL}`);
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data: BodyStyle[] = await response.json();
    return data;
};