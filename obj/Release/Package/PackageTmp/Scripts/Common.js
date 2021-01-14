function pagination(total, dispCnt, page, callback) {
    // 表示ページを算出
    var totalPageNo = Math.ceil(total / dispCnt);
    // 表示するページ数
    let dpCnt = 11;
    let sideCnt = 5;

    // 表示するページ番号の最小と最大を設定
    var minPage = 1;
    var maxPage = totalPageNo;
    if (totalPageNo > dpCnt) {
        if (1 < page - sideCnt) {
            if (totalPageNo > page + sideCnt) {
                minPage = page - sideCnt;
                maxPage = page + sideCnt;
            } else {
                minPage = totalPageNo - (dpCnt - 1);
                maxPage = totalPageNo;
            }
        } else {
            maxPage = dpCnt;
        }
    }

    //// HTML構築
    // 表示領域
    var ul = $('ul.pagination');
    ul.empty();

    // 前ページボタン
    if (1 != page) {
        let li = $('<li>').addClass('page-item');
        let span = $('<span>').addClass('page-link').attr('onclick', 'movePage(' + (Number(page) - 1) + ', ' + callback + ')').text("PREV");
        ul.append(li.append(span));
    }
    // page部生成
    for (var index = minPage; index < maxPage + 1; index++) {
        li = $('<li>').addClass('page-item');
        span = $('<span>').addClass('page-link').text(index);
        if (index == page) {
            li.addClass("active").attr('aria-current', 'page');
        } else {
            span.attr('onclick', 'movePage(' + (index) + ', ' + callback + ')');
        }
        ul.append(li.append(span));
    }
    // 次ページボタン
    if (totalPageNo != page) {
        li = $('<li>').addClass('page-item');
        span = $('<span>').addClass('page-link').attr('onclick', 'movePage(' + (Number(page) + 1) + ', ' + callback + ')').text("NEXT");
        ul.append(li.append(span));
    }
}



// ページャー用
var pageNo = 1;
var totalPageNo = 0;
var totalListCount = 0;
var searchListCount = 0;
var pageListCount = 0;
var startPageNo = 1;
var endPageNo = 0;
