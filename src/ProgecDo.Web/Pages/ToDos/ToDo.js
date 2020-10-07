let l = abp.localization.getResource('ProgecDo');

$(function () {

    let createListModal = new abp.ModalManager(abp.appPath + 'ToDos/CreateListModal');
    let editListModal = new abp.ModalManager(abp.appPath + 'ToDos/EditToDoListModal');
    let createToDoItemModal = new abp.ModalManager(abp.appPath + 'ToDos/CreateToDoItemModal');

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

    $('#NewToDoItemButton').click(function (e) {
        let id = this.getAttribute("data-id");
        createToDoItemModal.open({toDoListId: id});
        e.preventDefault();
    });
    
});
 