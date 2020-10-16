let l = abp.localization.getResource('ProgecDo');
let editToDoItemModal = new abp.ModalManager(abp.appPath + 'ToDos/EditToDoItemModal');

$(function () {

    let createListModal = new abp.ModalManager(abp.appPath + 'ToDos/CreateListModal');
    let editListModal = new abp.ModalManager(abp.appPath + 'ToDos/EditToDoListModal');
    let createToDoItemModal = new abp.ModalManager(abp.appPath + 'ToDos/CreateToDoItemModal');

    $('#NewToDoListButton').click(function (e) {
        let id = this.getAttribute("data-id");
        createListModal.open({projectId: id});
        e.preventDefault();
    });

    $('.EditToDoListButton').click(function (e) {
        let id = this.getAttribute("data-id");
        editListModal.open({toDoListId: id});
        e.preventDefault();
    });

    $('#NewToDoItemButton').click(function (e) {
        let id = this.getAttribute("data-id");
        createToDoItemModal.open({toDoListId: id});
        e.preventDefault();
    });

    $('.CreateToDoItemButton').click(function (e) {
        let id = this.getAttribute("data-id");
        createToDoItemModal.open({toDoListId: id});
        e.preventDefault();
    });
});

function editToDoItemDropdown(toDoListId, toDoItemId) {
    editToDoItemModal.open({toDoListId: toDoListId, toDoItemId: toDoItemId});
}

function deleteToDoItemDropdown(toDoItemId, toDoListId) {
    abp.message.confirm(l('ToDoItemDeletionConfirmationMessage'), function (result) {
        if (result === true) {
            progecDo.toDos.toDo.deleteToDoItem(toDoItemId, toDoListId).then(function () { 
                location.href= "/ToDos/ShowToDoList?toDoListId=" + toDoListId;
            });
        }
    });
}