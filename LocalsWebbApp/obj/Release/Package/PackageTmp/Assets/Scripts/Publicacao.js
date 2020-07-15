$(document).ready(function () {
    $('#fase2 .form-control').val('');

    $('.panel #FileUpload').on('change', function () {
        //get the file name
        var fileName = $(this).val().split('\\');
        //replace the "Choose a file" label
        $(this).next('.panel .custom-file-label').html(fileName[fileName.length - 1]);
    });

    $('.panel #FileUpload').on('change', function () {
        readURL(this);
        
        $('.panel #fase2').fadeIn();
    });

    $('#fase2 #btnAvancar').on('click', function (e) {
        if (validaForm('#fase2')) {
            $('.panel #fase1').hide(); 
            $('.panel #fase2').hide();
            $('.panel #fase3').fadeIn();
        }
        else
            e.preventDefault();
    });

    $('#fase3 #btnVoltar').on('click', function () {
        $('.panel #fase3').hide();
        $('.panel #fase1').fadeIn(); 
        $('.panel #fase2').fadeIn();
    });

    $('#fase3 #btnPublicar').on('click', function (e) {
        if (markers.length > 0) {
            var cords = markers[markers.length - 1].getPosition().lat() + '|' + markers[markers.length - 1].getPosition().lng();
            $('#fase2 #txtCords').val(cords);

            $('form').submit();   
        }
        else {
            showMensagem('danger', "É necessário definir a localização do problema.");
            e.preventDefault();
        }
    });


    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.panel #imgPublicacao').css('background-image', 'url(' + e.target.result + ')');
            };

            reader.readAsDataURL(input.files[0]);
        }
    }
});