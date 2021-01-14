//データ取得処理
function submitData() {
    var userName = $('#userName').val();
    var mail = $('#mail').val();
    var password = $('#password').val();
    if (userName == "" && mail == "" && password == "") {
        alert("名前を入力してください。\r\nメールアドレスを入力してください。\r\nパスワードを入力してください。");
        return false;
    } else if (userName == "" && mail == "" && password != "") {
        alert("名前を入力してください。\r\nメールアドレスを入力してください。");
        return false;
    } else if (userName == "" && mail != "" && password != "") {
        alert("名前を入力してください。");
        return false;
    } else if (userName == "" && mail != "" && password == "") {
        alert("名前を入力してください。\r\nパスワードを入力してください。");
        return false;
    } else if (userName != "" && mail != "" && password == "") {
        alert("パスワードを入力してください。");
        return false;
    } else if (userName != "" && mail == "" && password != "") {
        alert("メールアドレスを入力してください。");
        return false;
    } else if (userName != "" && mail == "" && password == "") {
        alert("メールアドレスを入力してください。\r\nパスワードを入力してください。");
        return false;
    } 

    $.ajax({
        type: "POST",
        url: "../Select/SubmitNewUser",
        data: { userName: userName, mail: mail, password: password },
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
            window.location.href = "../Select/EditUserList";
            return;
        }

    }).always(function () {
        hideLoader();
    });
}