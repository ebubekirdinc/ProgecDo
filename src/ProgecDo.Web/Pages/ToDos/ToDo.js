let l = abp.localization.getResource('ProgecDo');

$(function () {

    let createListModal = new abp.ModalManager(abp.appPath + 'ToDos/CreateListModal');
    let editListModal = new abp.ModalManager(abp.appPath + 'ToDos/EditToDoListModal');
    
    $('#NewToDoListButton').click(function (e) {
        let id = this.getAttribute("data-id");
        createListModal.open({projectId: id});
        e.preventDefault();
    });

    $('#EditToDoListButton').click(function (e) {
        let id = this.getAttribute("data-id");
        editListModal.open({toDoListId: id});
        e.preventDefault();
    });
});
 