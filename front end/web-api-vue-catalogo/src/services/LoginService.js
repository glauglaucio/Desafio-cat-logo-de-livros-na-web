import http from '@/http-common';

export default class LoginService {
    async Login(data) {
        const response = await http.post('api/v1/auth', {
            email: data.email,
            password: data.password
        });
        return response.data;
    }

    async VerificarEmail(email) {
        const response = await http.get(`api/v1/auth/verificar-email?email=${email}`);
        return response.data;
    }
}