import axios from "axios";

const createAxios = axios.create({
    baseURL: "https://localhost:7124",
});

export default createAxios;