//データ取得処理
function getData(count) {
    var usCd = sessionStorage.getItem("userCd");

    $.ajax({
        type: "POST",
        url: "../SelectSp/GetDataFromAjaxSp",
        data: { count: count, usCd: usCd },
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
            let li = $('<div class="cp_pagination">');
            for (var i = 0; i < pageCnt; i++) {
                var page = i + 1;
                if (i > 0) {
                    var prev = i - 1;
                    li.append($('<a class="cp_pagenum prev" onclick=getData("' + prev + '")>prev</a>>'));
                    if (i == count) {
                        li.append($('<span aria-current="page" class="cp_pagenum current" onclick=getData("' + i + '") id="page_' + i + '"><span>' + page + '</span></a></li>'));
                    } else {
                        li.append($('<a class="cp_pagenum" class="cp_pagenum current" onclick=getData("' + i + '") id="page_' + i + '"><span>' + page + '</span></a></li>'));
                    }
                } else {
                    if (i == count) {
                        li.append($('<span aria-current="page" class="cp_pagenum current" onclick=getData("' + i + '") id="page_' + i + '"><span>' + page + '</span></a></li>'));
                    } else {
                        li.append($('<a class="cp_pagenum" class="cp_pagenum current" onclick=getData("' + i + '") id="page_' + i + '"><span>' + page + '</span></a></li>'));
                    }
                }
                if (pageCnt > 1) {
                    li.append($('<a class="cp_pagenum next" onclick=getData("' + page + '")>next</a>'));
                }
            }
            li.append($('</ul>'));
            list.append(li);
            var idSelect = "page_" + cnt;
            $('#' + idSelect).addClass("active");
            return;
        }

    }).always(function () {
        hideLoader();
    });
}

function searchCheck() {
    if ($('#statusSearch').val() == "" && $('#chargeSearch').val() == "") {
        getData(0);
    } else {
        searchData(0);
    }
}

//データ検索
function searchData(count) {
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
        data: { statusSearch: statusSearch, chargeSearch: chargeSearch, count: count },
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
            //一覧クリア
            $('#dataList').empty();
            //一覧構築
            createList(resultData.DATA.listData);
            //ページネーション
            $('#paging').empty();
            var list = $('#paging');
            var cnt = resultData.DATA.dataCount;
            var pageCnt = Math.ceil(cnt / 10);
            var prev = count - 1;
            var fwd = count + 1;
            let li = $('<ul class="pagination">');
            for (var i = 0; i < pageCnt; i++) {
                var page = i + 1;
                if (i == count) {
                    li.append($('<li><a class="active" onclick=searchData("' + i + '") id="page_' + i + '" style="cursor: pointer;"><span>' + page + '</span></a></li>'));
                } else {
                    li.append($('<li><a onclick=searchData("' + i + '") id="page_' + i + '" style="cursor: pointer;"><span>' + page + '</span></a></li>'));
                }
            }
            li.append($('</ul>'));
            list.append(li);
            var idSelect = "page_" + cnt;
            $('#' + idSelect).addClass("active");
            return;
        }

    }).always(function () {
        hideLoader();
    });
}

//降順切り替え
function searchDataDesc(count) {
    var cnt;
    $.ajax({
        type: "POST",
        url: "../Select/DataSortDesc",
        data: { count: count },
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
            //一覧クリア
            $('#dataList').empty();
            //一覧構築
            createList(resultData.DATA.listData);
            //ページネーション
            $('#paging').empty();
            var list = $('#paging');
            cnt = resultData.DATA.dataCount;
            var pageCnt = Math.ceil(cnt / 10);
            var prev = count - 1;
            var fwd = count + 1;
            let li = $('<ul class="pagination">');
            for (var i = 0; i < pageCnt; i++) {
                var page = i + 1;
                if (i == count) {
                    li.append($('<li><a class="active" onclick=searchDataDesc("' + i + '") id="page_' + i + '" style="cursor: pointer;"><span>' + page + '</span></a></li>'));
                } else {
                    li.append($('<li><a onclick=searchDataDesc("' + i + '") id="page_' + i + '" style="cursor: pointer;"><span>' + page + '</span></a></li>'));
                }
            }
            li.append($('</ul>'));
            list.append(li);
            var idSelect = "page_" + cnt;
            $('#' + idSelect).addClass("active");
            return;
        }

    }).always(function () {
        hideLoader();
    });
}

//昇順切り替え
function searchDataAsc(count) {

    $.ajax({
        type: "POST",
        url: "../Select/DataSortAsc",
        data: { count: count },
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
            //一覧クリア
            $('#dataList').empty();
            //一覧構築
            createList(resultData.DATA.listData);
            //ページネーション
            $('#paging').empty();
            var list = $('#paging');
            var cnt = resultData.DATA.dataCount;
            var pageCnt = Math.ceil(cnt / 10);
            var prev = count - 1;
            var fwd = count + 1;
            let li = $('<ul class="pagination">');
            for (var i = 0; i < pageCnt; i++) {
                var page = i + 1;
                if (i == count) {
                    li.append($('<li><a class="active" onclick=searchDataAsc("' + i + '") id="page_' + i + '" style="cursor: pointer;"><span>' + page + '</span></a></li>'));
                } else {
                    li.append($('<li><a onclick=searchDataAsc("' + i + '") id="page_' + i + '" style="cursor: pointer;"><span>' + page + '</span></a></li>'));
                }
            }
            li.append($('</ul>'));
            list.append(li);
            var idSelect = "page_" + cnt;
            $('#' + idSelect).addClass("active");
            return;
        }

    }).always(function () {
        hideLoader();
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
                showLoader();
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
            hideLoader();
        });
    }
}

function createList(data) {
    var list = $('#dataList');
    data.filter(function (d, index) {
        let tr = "";
        if (d.charge == "未定" || d.status == "状態未確定") {
            tr = $('<tr class="text-center" name="nocharge" style="background-color:#ffff00">');
        } else {
            tr = $('<tr class="text-center">');
        }
        tr.append($('<td class="text-center"><a href="' + d.companyUrl + '" target="_blank">' + d.companyName + '</a>'));
        tr.append($('<td class="text-center">').append(d.status));
        tr.append($('<td class="text-center">').append(d.note));
        tr.append($('<td class="text-center"><button type="button" class="btn btn-info" onclick="editData(' + d.cd + ')">編集</button>'));
        //一覧に追加
        list.append(tr);
    })
}

function createData() {
    window.location.href = "../Select/CreateData";
}