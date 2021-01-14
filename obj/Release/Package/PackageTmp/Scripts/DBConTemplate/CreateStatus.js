//データ取得処理
function submitData() {
    var statusName = $('#statusName').val();
    if (statusName == "") {
        alert("状態名称を入力してください。");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "../Select/SubmitNewStatus",
        data: { statusName: statusName },
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
            alert("データの作成に成功しました。");
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