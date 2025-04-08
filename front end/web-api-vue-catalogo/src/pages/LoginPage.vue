<template>
    <div class="row col-12 justify-center">
        <form @submit.prevent="authenticate" class="row">
            <div class="col-12 q-mt-md">
                <q-input v-model="data.email" outlined bg-color="white" label="Email" id="email" dense
                         input-class="input-default" />
            </div>
            <div class="col-12 q-mt-md">
                <q-input v-model="data.password" type="password" bg-color="white" outlined label="Senha" id="password" dense
                         input-class="input-default" />
            </div>
            <div class="col-12 q-mt-sm text-right">
                <q-btn flat color="primary" label="Esqueci a senha" @click="resetarSenha" />
            </div>
            <div class="flex row col-12 justify-end q-pt-sm">
                <q-btn flat color="secondary" label="Cadastrar" @click="cadastrar" />
            </div>

            <div class="flex row col-12 justify-end q-pt-lg">
                <q-btn :disable="loading" type="submit" color="primary">
                    <q-spinner-hourglass v-if="loading" color="white" size="1.5rem" />
                    <span v-else>Login</span>
                </q-btn>
            </div>
        </form>
    </div>
</template>

<script setup>
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';
    import LoginService from '@/services/LoginService';

    let _loginService = new LoginService();
    let loading = ref(false);

    let data = ref({
        email: "",
        password: "",
    });

    const router = useRouter();

    async function authenticate() {
        loading.value = true;
        await _loginService.Login(data.value).then((response) => {
            console.log(response);
            alert(response.token.token);
        }).catch(() => {
            alert("Falha");
        });
        loading.value = false;
    }

    function resetarSenha() {
        router.push('/esqueci-senha');
    }

    function cadastrar() {
        router.push('/cadastro');
    }
</script>