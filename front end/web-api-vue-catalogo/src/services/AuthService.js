import http from '@/http-common';

export default class AuthService {
    async VerificarCodigo(codigo) {
        const response = await http.post('api/v1/auth/verificar-codigo', {
            Codigo: codigo
        });
        return response.data;
    }
}