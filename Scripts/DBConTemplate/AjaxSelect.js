//データ取得処理
function getData(count) {
    $.ajax({
        type: "POST",
        url: "../Select/GetDataFromAjax",
        data: { count: count},
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
            //一覧クリア
            $('#dataList').empty();
            $('#paging').empty();
            //一覧構築
            createList(resultData.DATA.listData);
            //ページネーション
            var list = $('#paging');
            var cnt = resultData.DATA.dataCount;
            var pageCnt = Math.ceil(cnt / 10);
            var prev = count - 1;
            var fwd = count + 1;
            let li = $('<ul class="pagination">');
            for (var i = 0; i < pageCnt; i++) {
                var page = i + 1;
                if (i == count) {
                    li.append($('<li><a class="active" onclick=getData("' + i + '") id="page_'+i+'"><span>' + page + '</span></a></li>'));
                } else {
                    li.append($('<li><a onclick=getData("' + i + '") id="page_' + i +'"><span>' + page + '</span></a></li>'));
                }
            }
            li.append($('</ul>'));
            list.append(li);
            return;
        }

    }).always(function () {
        //成功、失敗にかかわらず常に行う処理
    });
    var idSelect = "page_" + cnt;
    $('#' + idSelect).addClass("active");
}

function searchCheck() {
    if ($('#statusSearch').val() == "" && $('#chargeSearch').val() == "") {
        getData(0);
    } else {
        searchData();
    }
}

//データ検索
function searchData() {
    var statusSearch = "";
    var chargeSearch = "";
    if ($('#statusSearch').val() != null && $('#statusSearch').val() != "") {
        statusSearch = $('#statusSearch').val();
    }
    if ($('#chargeSearch').val() != null && $('#chargeSearch').val() != "") {
        chargeSearch = $('#chargeSearch').val();
    }

    $.ajax({
        type: "POST",
        url: "../Select/SearchData",
        data: { statusSearch: statusSearch, chargeSearch: chargeSearch },
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
            //一覧クリア
            $('#dataList').empty();
            //一覧構築
            createList(resultData.DATA.listData);
            return;
        }

    }).always(function () {
        //成功、失敗にかかわらず常に行う処理
    });
}

//データ削除処理
function delData(cd) {
    if (!confirm("データを削除してよろしいですか？")) {
        return false;

    } else {
        $.ajax({
            type: "POST",
            url: "../Select/DeleteData",
            data: { cd: cd },
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
                getData(0);
                return;
            }

        }).always(function () {
            //成功、失敗にかかわらず常に行う処理
        });
    }
}

function createList(data) {
    var list = $('#dataList');
    data.filter(function (d, index) {
        let tr = "";
        if (d.charge != "") {
            tr = $('<tr class="text-center">');
        } else {
            tr = $('<tr class="text-center" name="nocharge" style="background-color:#ffff00">');
        }
        tr.append($('<td class="text-center"><a href="' + d.companyUrl + '" target="_blank">' + d.companyName + '</a>'));
        tr.append($('<td class="text-center">').append(d.status));
        tr.append($('<td class="text-center">').append(d.charge));
        tr.append($('<td class="text-center">').append(d.note));
        tr.append($('<td class="text-center">').append(d.date));
        tr.append($('<td class="text-center"><button type="button" class="btn btn-info" onclick="editData(' + d.cd + ')">編集</button>'));
        tr.append($('<td class="text-center"><button type="button" class="btn btn-info" onclick="delData(' + d.cd + ')">削除</button>'));
        //一覧に追加
        list.append(tr);
    })
}

function createData() {
    window.location.href = "../Select/CreateData";
}