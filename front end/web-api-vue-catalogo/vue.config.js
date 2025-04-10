const { defineConfig } = require('@vue/cli-service');

module.exports = defineConfig({
    transpileDependencies: ['quasar'],
    pluginOptions: {
        quasar: {
            importStrategy: 'kebab',
            rtlSupport: true
        }
    },
    devServer: {
        open: true
    }
});