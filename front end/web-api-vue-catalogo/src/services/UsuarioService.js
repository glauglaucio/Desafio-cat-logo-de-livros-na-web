import http from '@/http-common';

export default class UsuarioService {
    async Cadastrar(usuario) {
        const response = await http.post('api/v1/cadastro', usuario);
        return response.data;
    }
}