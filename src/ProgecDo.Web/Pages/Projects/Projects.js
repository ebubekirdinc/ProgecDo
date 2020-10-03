$(function () {
    let l = abp.localization.getResource('ProgecDo');
    let createModal = new abp.ModalManager(abp.appPath + 'Projects/CreateModal');
    let editModal = new abp.ModalManager(abp.appPath + 'Projects/EditModal');
    let container = $('#projectsComponentContainer');

    $('#NewProjectButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    createModal.onResult(function () {
        reloadProjects();
        abp.notify.info(
            l('ProjectSuccessfullyCreated')
        );
    });

    container.on('click', '.editProjectButton', function () {
        let id = this.getAttribute("data-id");
        editModal.open({id: id});
        // e.preventDefault();
        // console.log(id);
    });

    editModal.onResult(function () {
        reloadProjects();
        abp.notify.info(
            l('ProjectSuccessfullyUpdated')
        );
    });

    function reloadProjects() {
        $.get("/Index?handler=ProjectsViewComponent", function (data) {
            container.html(data);
        });
    } 
});
