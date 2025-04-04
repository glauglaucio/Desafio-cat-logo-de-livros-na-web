<template>
    <div class="row col-12 justify-center">
        <form @submit.prevent="verificarCodigo" class="row">
            <div class="col-12 q-mt-md">
                <q-input v-model="codigo" outlined bg-color="white" label="Digite o Código" dense
                         input-class="input-default" />
            </div>
            <div class="flex row col-12 justify-end q-pt-lg">
                <q-btn type="submit" color="primary" label="Verificar Código" />
            </div>
        </form>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import AuthService from '@/services/AuthService';

const codigo = ref("");
const router = useRouter();

const authService = new AuthService();

    async function verificarCodigo() {
        try {
            const response = await authService.VerificarCodigo(codigo.value);
            alert(response.mensagem);
            router.push('/redefinir-senha');
        } catch (error) {
            alert('Erro ao verificar o código: ' + error.response.data);
        }
    }
</script>