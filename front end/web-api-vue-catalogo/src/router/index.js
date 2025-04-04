import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '@/pages/LoginPage.vue';
import EsqueciSenha from '@/pages/EsqueciSenha.vue';
import VerificarCodigo from '@/pages/VerificarCodigo.vue';
import RedefinirSenha from '@/pages/RedefinirSenha.vue';

const routes = [
    { path: '/', component: LoginPage },
    { path: '/esqueci-senha', component: EsqueciSenha },
    { path: '/verificar-codigo', component: VerificarCodigo },
    { path: '/redefinir-senha', component: RedefinirSenha},
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;