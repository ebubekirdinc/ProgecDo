let editBoardMessageModal = new abp.ModalManager(abp.appPath + 'BoardMessages/EditBoardMessageModal');
let editCommentModal = new abp.ModalManager(abp.appPath + 'BoardMessages/EditCommentModal');
let l = abp.localization.getResource('ProgecDo');

$(function () {

    let l = abp.localization.getResource('ProgecDo');
    let createModal = new abp.ModalManager(abp.appPath + 'BoardMessages/CreateModal');
    // let container = $('#projectsComponentContainer');

    if (location.hash === '#boardMessagesCreateModal') {
        location.hash = '';
        abp.notify.info(
            l('SuccessfullyCreated')
        );
    } else if (location.hash === '#editBoardMessageDropdown') {
        location.hash = '';
        abp.notify.info(
            l('SuccessfullyUpdated')
        );
    } else if (location.hash === '#deleteBoardMessageDropdown') {
        location.hash = '';
        abp.notify.info(
            l('SuccessfullyDeleted')
        );
    } else if (location.hash === '#deleteBoardMessageCommentDropdown') {
        location.hash = '';
        abp.notify.info(
            l('SuccessfullyDeleted')
        );
    } else if (location.hash === '#editBoardMessageCommentDropdown') {
        location.hash = '';
        abp.notify.info(
            l('SuccessfullyUpdated')
        );
    }


    $('#NewBoardMessageButton').click(function (e) {
        let id = this.getAttribute("data-id");
        createModal.open({projectId: id});
        e.preventDefault();
    });

    createModal.onResult(function () {
        location.hash = 'boardMessagesCreateModal';
        location.reload();
    });

    let messages = document.getElementsByClassName("boardMessage");

    for (let i = 0; i < messages.length; i++) {
        messages[i].addEventListener('click',
            function (event) {
                let id = this.getAttribute("data-id");
                window.location.href = "/BoardMessages/ShowBoardMessage?parentId=" + id;
            },
            false);
    }

    // ShowBoardMessage
    editBoardMessageModal.onResult(function () {
        location.hash = 'editBoardMessageDropdown';
        location.reload();
    });

    editCommentModal.onResult(function () {
        location.hash = 'editBoardMessageCommentDropdown';
        location.reload();
    });


});

// ShowBoardMessage
function editBoardMessageDropdown(parentId) {
    editBoardMessageModal.open({parentId: parentId});
}

function deleteBoardMessageDropdown(parentId,projectId) {
    abp.message.confirm(l('BoardMessageDeletionConfirmationMessage'), function (result) {
        if (result === true) {
            progecDo.boardMessages.boardMessage.delete(parentId).then(function () { 
                window.location.href= "/BoardMessages/Index?projectId=" + projectId;
            });
        }
    });
}

function editBoardMessageCommentDropdown(commentId, parentId) {
    editCommentModal.open({commentId: commentId, parentId: parentId});
}

function deleteBoardMessageCommentDropdown(commentId, parentId, content) {
    abp.message.confirm(l('BoardMessageCommentDeletionConfirmationMessage', content), function (result) {
        if (result === true) {
            progecDo.boardMessages.boardMessage.deleteBoardmessageComment(commentId, parentId).then(function () {
                location.hash = 'deleteBoardMessageCommentDropdown';
                location.reload();
            });
        }
    });
}