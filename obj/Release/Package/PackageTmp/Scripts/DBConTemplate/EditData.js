﻿//データ取得処理
function submitData(cd) {
    var companyName = $('#companyName').val();
    var companyUrl = $('#companyUrl').val();
    var status = $('#status').val();
    var charge = $('#charge').val();
    var note = $('#note').val();
    var mail = $('#mail').val();
    if (companyName == "" && companyUrl == "" && mail == "") {
        alert("企業名を入力してください。\r\n企業URLを入力してください。\r\nメールアドレスを入力してください。");
        return false;
    } else if (companyName != "" && companyUrl == "" && mail == "") {
        alert("企業URLを入力してください。\r\nメールアドレスを入力してください。");
        return false;
    } else if (companyName != "" && companyUrl != "" && mail == "") {
        alert("メールアドレスを入力してください。");
        return false;
    } else if (companyName == "" && companyUrl != "" && mail == "") {
        alert("企業名を入力してください\r\nメールアドレスを入力してください。");
        return false;
    } else if (companyName == "" && companyUrl != "" && mail != "") {
        alert("企業名を入力してください。");
        return false;
    } else if (companyName == "" && companyUrl == "" && mail != "") {
        alert("企業名を入力してください。\r\n企業URLを入力してください。");
        return false;
    } else if (companyName != "" && companyUrl == "" && mail != "") {
        alert("企業URLを入力してください。");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "../Select/EditDataSubmit",
        data: { cd: cd, companyName: companyName, companyUrl: companyUrl, status: status, charge: charge, note: note, mail: mail },
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
            window.location.href = "../Select/AjaxSelect";
            return;
        }

    }).always(function () {
        hideLoader();
    });
}

function toList() {
    window.location.href = "../Select/AjaxSelect";
}