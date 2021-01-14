//データ取得処理
function submitData(cd) {
    var statusName = $('#statusName').val();

    $.ajax({
        type: "POST",
        url: "../Select/EditStatusSubmit",
        data: { cd: cd, statusName: statusName },
        beforeSend: function () {
            //POST前の動作(ローダー出したり)
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
        //成功、失敗にかかわらず常に行う処理
    });
}

function toList() {
    window.location.href = "../Select/EditStatusList";
}