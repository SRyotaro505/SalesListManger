//データ取得処理
function submitData() {
    var companyName = $('#companyName').val();
    var companyUrl = $('#companyUrl').val();
    var status = $('#status').val();
    var charge = $('#charge').val();
    var note = $('#note').val();
    if (companyName == "" && companyUrl == "") {
        alert("企業名を入力してください。\r\n企業URLを入力してください。");
        return false;
    } else if (companyName == "") {
        alert("企業名を入力してください。");
        return false;
    } else if (companyUrl == "") {
        alert("企業URLを入力してください。");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "../Select/SubmitNewData",
        data: { companyName: companyName, companyUrl: companyUrl, status: status, charge: charge, note: note },
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
            alert("データの作成に成功しました。");
            return;
        }

    }).always(function () {
        hideLoader();
    });
}

function toList() {
    window.location.href = "../Select/AjaxSelect";
}