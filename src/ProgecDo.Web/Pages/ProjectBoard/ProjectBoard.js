let assignUserToProjectModal = new abp.ModalManager(abp.appPath + 'ProjectBoard/AssignUserToProjectModal');

$(function () {
    let l = abp.localization.getResource('ProgecDo');

    assignUserToProjectModal.onResult(function () {
        abp.notify.info(
            l('UserSuccessfullyAddedToProject')
        );
    });

    document.getElementById('messagesCard')
        .addEventListener('click', function (event) {
            let id = this.getAttribute("data-id");
            window.location.href = "/BoardMessages/Index?projectId=" + id;
        });
    
    document.getElementById('toDosCard')
        .addEventListener('click', function (event) {
            let id = this.getAttribute("data-id");
            window.location.href = "/ToDos/Index?projectId=" + id;
        });
});

function assignUserToProject(id) {
    assignUserToProjectModal.open({projectId: id});
}