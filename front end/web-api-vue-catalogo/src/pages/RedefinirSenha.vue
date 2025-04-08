<template>
    <div class="row col-12 justify-center">
        <form @submit.prevent="RedefinirSenha" class="row">
            <div class="col-12 q-mt-md">
                <q-input v-model="data.novaSenha" type="password" bg-color="white" outlined label="Nova Senha" id="novaSenha" dense
                         input-class="input-default" />
            </div>
            <div class="col-12 q-mt-md">
                <q-input v-model="data.confirmarSenha" type="password" bg-color="white" outlined label="Confirmar Senha" id="confirmarSenha" dense
                         input-class="input-default" />
            </div>
            <div class="flex row col-12 justify-end q-pt-lg">
                <q-btn type="submit" color="primary">
                    Trocar Senha
                </q-btn>
            </div>
        </form>
    </div>
</template>

<script setup>
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';
    import ForgotPasswordService from '@/services/ForgotPasswordService';

    const forgotPasswordService = new ForgotPasswordService();
    const router = useRouter();

    let data = ref({
        novaSenha: "",
        confirmarSenha: ""
    });

    async function RedefinirSenha() {
        if (data.value.novaSenha !== data.value.confirmarSenha) {
            alert("As senhas não coincidem!");
            return;
        }

        try {
            await forgotPasswordService.RedefinirSenha(data.value.novaSenha);
            alert("Senha redefinida com sucesso!");
            router.push('/');
        } catch (error) {
            if (error.response && error.response.data) {
                alert(error.response.data);
            } else {
                alert("Erro ao redefinir senha.");
            }
        } 
    }
</script>