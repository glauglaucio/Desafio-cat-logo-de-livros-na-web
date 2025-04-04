import http from '@/http-common';

export default class ForgotPasswordService {
    async EnviarCodigo(email) {
        const response = await http.post('/api/v1/auth/esqueci-senha', { email });
        return response.data;  
    }

    async VerificarCodigo(email, codigo) {
        const response = await http.post('/api/v1/auth/verificar-codigo', { email, codigo });
        return response.data; 
    }

    async RedefinirSenha(senha) {
        const response = await http.post('/api/v1/auth/redefinir-senha', { novaSenha: senha });
        return response.data;
    }
}