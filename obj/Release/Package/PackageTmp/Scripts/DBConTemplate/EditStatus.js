//データ取得処理
function submitData(cd) {
    var statusName = $('#statusName').val();

    $.ajax({
        type: "POST",
        url: "../Select/EditStatusSubmit",
        data: { cd: cd, statusName: statusName },
        beforeSend: function () {
            showLoader();
        }

    }).fail(function () {
        alert("データの取得に失敗しました。")

    }).done(function (resultData) {
        if (resultData.ErrorMessage != null && resultData.ErrorMessage != "") {
            alert(resultData.ErrorMessage);
            return;
        } else {
            alert("データの更新に成功しました。");
            window.location.href = "../Select/EditStatusList";
            return;
        }

    }).always(function () {
        hideLoader();
    });
}

function toList() {
    window.location.href = "../Select/EditStatusList";
}