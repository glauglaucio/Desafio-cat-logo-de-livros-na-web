<template>
    <div class="row col-12 justify-center">
        <form @submit.prevent="CadastrarUsuario" class="row">
            <div class="col-12 q-mt-md">
                <q-input v-model="data.nome" bg-color="white" outlined label="Nome" id="nome" dense input-class="input-default" />
            </div>
            <div class="col-12 q-mt-md">
                <q-input v-model="data.datanascimento" type="date" bg-color="white" outlined label="Data de Nascimento" id="dataNascimento" dense input-class="input-default" />
            </div>
            <div class="col-12 q-mt-md">
                <q-input v-model="data.email" type="email" bg-color="white" outlined label="Email" id="email" dense input-class="input-default" />
            </div>
            <div class="col-12 q-mt-md">
                <q-input v-model="data.senha" type="password" bg-color="white" outlined label="Senha" id="senha" dense input-class="input-default" />
            </div>
            <div class="col-12 q-mt-md">
                <q-input v-model="data.confirmarSenha" type="password" bg-color="white" outlined label="Confirmar Senha" id="confirmarSenha" dense input-class="input-default" />
            </div>
            <div class="flex row col-12 justify-end q-pt-lg">
                <q-btn type="submit" color="primary">
                    Cadastrar
                </q-btn>
            </div>
        </form>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import UsuarioService from '@/services/UsuarioService';

const usuarioService = new UsuarioService();
const router = useRouter();

const data = ref({
    nome: '',
    datanascimento: '',
    email: '',
    senha: '',
    confirmarSenha: ''
});

async function CadastrarUsuario() {
    if (data.value.senha !== data.value.confirmarSenha) {
        alert('As senhas não coincidem!');
        return;
    }

    if (data.value.senha.length < 6) {
        alert('A senha deve ter pelo menos 6 caracteres!');
        return;
    }

    try {
        await usuarioService.Cadastrar({
            nome: data.value.nome,
            datanascimento: new Date(data.value.datanascimento).toISOString(),
            email: data.value.email,
            senha: data.value.senha
        });

        alert('Usuário cadastrado com sucesso!');
        router.push('/');
    } catch (error) {
        if (error.response && error.response.data) {
            alert('Erro ao cadastrar: ' + error.response.data);
        } else {
            alert('Erro desconhecido ao cadastrar.');
        }
    }
}
</script>