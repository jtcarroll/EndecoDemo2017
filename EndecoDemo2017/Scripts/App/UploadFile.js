var r = new Resumable({
    target: '/Stock/UploadFile/',
    query: { pricetype: 'ex' },
    //headers: { pricetype: 'ex' }, 
    maxChunkRetries: 3,
    maxFiles: 1,
    prioritizeFirstAndLastChunk: false,
    simultaneousUploads: 1,
    chunkSize: 1 * 20 * 1024
});
//console.log(r);
var results = $('#results'),
    draggable = $('#dragHere'),
    uploadFile = $('#uploadFiles'),
    browseButton = $('#browseButton'),
    nothingToUpload = $('[data-nothingToUpload]');


// if resumable is not supported aka IE
if (!r.support) location.href = 'http://browsehappy.com/';

r.assignBrowse(browseButton);
r.assignDrop(draggable);

r.on('fileAdded', function (file, event) {
    var template =
        '<div data-uniqueid="' + file.uniqueIdentifier + '">' +
        '<div class="large-6 right deleteFile">X</div>' +
        '<div class="progress large-6">' +
        '<span class="meter" style="width:0%;color: black;"></span>' +
        '</div>' +
        '</div>';

    results.append(template);
});

uploadFile.on('click', function () {
    if (results.children().length > 0) {
        r.query = {};
        r.upload();
    } else {
        nothingToUpload.fadeIn();
        setTimeout(function () {
            nothingToUpload.fadeOut();
        }, 3000);
    }
});

$(document).on('click', '.deleteFile', function () {
    var self = $(this),
        parent = self.parent(),
        identifier = parent.data('uniqueid'),
        file = r.getFromUniqueIdentifier(identifier);

    r.removeFile(file);
    parent.remove();
});


r.on('fileProgress', function (file) {
    var progress = Math.floor(file.progress() * 100);
    $('[data-uniqueId=' + file.uniqueIdentifier + ']').find('.meter').css('width', progress + '%');
    $('[data-uniqueId=' + file.uniqueIdentifier + ']').find('.meter').html('&nbsp;' + progress + '%');
});

r.on('fileSuccess', function (file, message) {
    $('[data-uniqueId=' + file.uniqueIdentifier + ']').find('.progress').addClass('success');
    fetchStockDataByHeaderId(0);
});


r.on('uploadStart', function () {
    $('.alert-box').text('Uploading....');
});

r.on('complete', function () {
    $('.alert-box').text('Done Uploading');
});

$('[data-toggle="popover"]').popover();   


$.ajax({
    url: '/Stock/GetStockHeaderList?fetchCount=5',
    context: document.body,
    success: function (response) {
        var r = new Array(), j = -1;
        for (var i = 0; i < response.Data.length; i++) {
            r[++j] = '<div class="row divBottomBorder"><div class="col-md-1">';
            r[++j] = '<a href="javascript:fetchStockDetails(' + response.Data[i].Id + ');">' + response.Data[i].Id + '</a>';
            r[++j] = '</div><div class="col-md-1">';
            r[++j] = response.Data[i].StockType;
            r[++j] = '</div><div class="col-md-3">';
            r[++j] = new Date(parseInt(response.Data[i].DateUploaded.substr(6))).toLocaleDateString();
            r[++j] = '</div><div class="col-md-6">';
            r[++j] = response.Data[i].FileNameUploaded;
            r[++j] = '</div><div class="col-md-1">';
            r[++j] = '<a href="javascript:fetchStockDetails(' + response.Data[i].Id + ');"><i class="material-icons">&#xe8e5;</i></a>';
            r[++j] = '</div></div>';

            //<i class="fa fa-area-chart"></i>
        }
        $('#uploadsListId').html(r.join('')); 
    }
});


function fetchStockDetails(headerId) {
    console.log(headerId);
    fetchStockDataByHeaderId(headerId);
}

function fetchStockDataByHeaderId(headerId) {
    d3.json('/Stock/GetStockDetails?headerid=' + headerId, function (json) {
        var data = json.Data.Stock;
        for (var i = 0; i < data.length; i++) {
            var date = new Date(data[i].Date);
            data[i].Date = date;
        }

        var markers = [{
            'Date': new Date(json.Data.MinPrice.Date),
            'label': "Minimum Price"
        }, {
            'Date': new Date(json.Data.MaxPrice.Date),
            'label': "Maximum Price"
        }, {
            'Date': new Date(json.Data.MostCostlyHour),
            'label': "Most Expensive Hour"
        }
        ];

        MG.data_graphic({
            title: "Electricity Price for every 30 minutes from January 2017",
            description: "This chart shows Electricity price for every 30 minutes",
            data: data,
            interpolate: d3.curveLinear,
            width: 1000,
            height: 600,
            right: 30,
            markers: markers,
            target: '#markers-clickable',
            x_accessor: 'Date',
            y_accessor: 'Value'
        });

        //change location of a marker as writing over each other
        var listNg = $('.mg-marker-text');
        for (var i = 0; i < listNg.length; i++) {
            if (listNg[i].innerHTML === 'Minimum Price') {
                listNg[i].setAttribute('y', 100);
            }
        }
    });
}