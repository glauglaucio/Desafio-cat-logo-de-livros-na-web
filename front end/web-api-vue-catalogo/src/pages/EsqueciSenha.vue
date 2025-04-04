<template>
    <div class="row col-12 justify-center">
        <form @submit.prevent="enviarCodigo" class="row">
            <div class="col-12 q-mt-md">
                <q-input v-model="email" outlined bg-color="white" label="Digite seu Email" dense
                         input-class="input-default" />
            </div>
            <div class="flex row col-12 justify-end q-pt-lg">
                <q-btn :disable="loading" type="submit" color="primary">
                    <q-spinner-hourglass v-if="loading" color="white" size="1.5rem" />
                    <span v-else>Enviar Código</span>
                </q-btn>
            </div>
        </form>
    </div>
</template>

<script setup>
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';
    import ForgotPasswordService from '@/services/ForgotPasswordService';

    let email = ref("");
    let loading = ref(false);
    const router = useRouter();
    const forgotPasswordService = new ForgotPasswordService();

    async function enviarCodigo() {
        loading.value = true;
        try {
            const data = await forgotPasswordService.EnviarCodigo(email.value);
            alert(data.mensagem ?? "Código enviado!");
            router.push('/verificar-codigo');
        } catch (erro) {
            alert(erro.response?.data ?? "Erro ao enviar código.");
        } finally {
            loading.value = false;
        }
    }
</script>