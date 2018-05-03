app.loading = [];
console.log('loaded stuff');
require.config({
    onNodeCreated: function (node, config, moduleName, url) {
        app.loading.push(moduleName);
        //console.log('module ' + moduleName + ' loading...');
        //console.log('loading: ' + app.loading.length);
        node.addEventListener('load', function () {
            app.loading = _.without(app.loading, moduleName);
            //console.log('module ' + moduleName + ' loaded!')
            //console.log('loading: ' + app.loading.length);
        });
        node.addEventListener('error', function () {
            app.loading = _.without(app.loading, moduleName);
            //console.log('module ' + moduleName + ' could not be loaded.');
            //console.log('loading: ' + app.loading.length);
        });
    }
});