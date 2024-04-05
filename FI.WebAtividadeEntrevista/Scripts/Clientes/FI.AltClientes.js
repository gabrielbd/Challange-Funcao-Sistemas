$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #CPF').val(obj.CPF).attr('readonly', true);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
        $('#formCadastro #clienteId').val(obj.Id);
    }
    $('#cpfBeneficiario').mask('000.000.000-00', { reverse: true });

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "CPF": $(this).find("#CPF").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val()
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                }
        });
    })

    $('#modalBeneficiarios').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var modal = $(this);
        modal.find('#tabelaBeneficiarios').empty();

        // Carregar os beneficiários do cliente via AJAX
        $.ajax({
            url: urlGetBeneficiarioById, 
            type: 'GET',
            data: { "idClient": obj.Id },
            success: function (response) {
                response.forEach(function (beneficiario) {
                    var row = '<tr>' +
                        '<td>' + beneficiario.CPF + '</td>' +
                        '<td>' + beneficiario.Nome + '</td>' +
                        '<td>' +
                        '<button class="btn btn-danger btn-excluir" data-id="' + beneficiario.Id + '">Excluir</button>' +
                        '<button class="btn btn-primary btn-alterar" data-id="' + beneficiario.Id + '" data-cpf="' + beneficiario.CPF + '" data-nome="' + beneficiario.Nome + '" data-toggle="modal" data-target="#modalAlterarBeneficiario">Alterar</button>' +
                        '</td>' +
                        '</tr>';
                    modal.find('#tabelaBeneficiarios').append(row);
                });
            },
            error: function (error) {
                console.log('Erro ao obter beneficiários:', error);
            }
        });
    });

    // Função para adicionar um novo beneficiário
     $('#formBeneficiario').submit(function (e) {
        e.preventDefault();

         var clientId = obj.Id;

        if (clientId) {
            $.ajax({
                url: urlPostBeneficiario,
                method: "POST",
                data: {
                    "NOME": $(this).find("#nomeBeneficiario").val(),
                    "CPF": $(this).find("#cpfBeneficiario").val(),
                    "IdCliente": clientId
                },
                error:
                    function (r) {
                        if (r.status == 400)
                            ModalDialog("Ocorreu um erro", r.responseJSON);
                        else if (r.status == 500)
                            ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                    },
                success:
                    function (r) {
                        ModalDialog("Sucesso!", r)
                        $("#formBeneficiario")[0].reset();
                        $.ajax({
                            url: urlGetBeneficiarioById,
                            method: "GET",
                            data: { "idClient": clientId },
                            success:
                                function (data) {
                                    $('#beneficiarioId').val(data.Id)
                                    $('#tabelaBeneficiarios').empty();
                                    data.forEach(function (beneficiario) {                                      
                                        var linha = '<tr>' +
                                            '<td>' + beneficiario.CPF + '</td>' +
                                            '<td>' + beneficiario.Nome +'</td>' +
                                            '<td>' +
                                            '<button class="btn btn-danger btn-excluir" data-id="' + beneficiario.Id + '">Excluir</button>' +
                                            '<button class="btn btn-primary btn-alterar" data-id="' + beneficiario.Id + '" data-cpf="' + beneficiario.CPF + '" data-nome="' + beneficiario.Nome + '" data-toggle="modal" data-target="#modalAlterarBeneficiario">Alterar</button>' +
                                            '</td>' +
                                            '</tr>';
                                        $('#tabelaBeneficiarios').append(linha);
                                    });
                                },
                            error:
                                function (r) {
                                    console.log("Erro ao obter beneficiários:", r);
                                }
                        });
                    }
            });
        } else {
            ModalDialog("Erro!", { Erro: ["Cliente ainda não está disponível. Cadastre o cliente primeiro."] });
        }
    });

    $(document).on('click', '.btn-excluir', function () {
        var idBeneficiario = $(this).data('id');
        $('#btnConfirmarExclusao').val(idBeneficiario);
        $('#modalExcluirBeneficiario').modal('show');
    });

    $('#btnConfirmarExclusao').click(function () {
        var idBeneficiario = $('#btnConfirmarExclusao').val();
        $.ajax({
            url: urlExcluirBeneficiario,
            method: "POST",
            data: {
                "id": idBeneficiario
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $('#modalExcluirBeneficiario').modal('hide');
                    $('#tabelaBeneficiarios').find('[data-id="' + idBeneficiario + '"]').closest('tr').remove();

                }
        });

    });


    $(document).on('click', '.btn-alterar', function () {
        var idBeneficiario = $(this).data('id');
        var cpfBeneficiario = $(this).data('cpf');
        var nomeBeneficiario = $(this).data('nome');
        $('#idBeneficiarioAlterar').val(idBeneficiario);
        $('#cpfBeneficiarioAlterar').val(cpfBeneficiario);
        $('#nomeBeneficiarioAlterar').val(nomeBeneficiario);
        $('#modalAlterarBeneficiario').modal('show');
    });

    $('#formAlterarBeneficiario').submit(function (e) {
        e.preventDefault();
        var idBeneficiario = $('#idBeneficiarioAlterar').val();
        var cpfBeneficiario = $('#cpfBeneficiarioAlterar').val();
        var $form = $(this);

        $.ajax({
            url: urlAlterarBeneficiario,
            method: "POST",
            data: {
                "NOME": $form.find("#nomeBeneficiarioAlterar").val(),
                "ID": idBeneficiario,
                "CPF": cpfBeneficiario
            },
            error: function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success: function (r) {
                ModalDialog("Sucesso!", r);
                $('#modalAlterarBeneficiario').modal('hide');

                var $row = $('#tabelaBeneficiarios').find('[data-id="' + idBeneficiario + '"]').closest('tr');
                $row.find('td:eq(0)').text(cpfBeneficiario);
                $row.find('td:eq(1)').text($form.find("#nomeBeneficiarioAlterar").val());
            }
        });
    });

})

function ModalDialog(titulo, data) {
    var random = Math.random().toString().replace('.', '');
    var modalHtml = '<div id="' + random + '" class="modal fade">' +
        '        <div class="modal-dialog">' +
        '            <div class="modal-content">' +
        '                <div class="modal-header">' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>' +
        '                    <h4 class="modal-title">' + titulo + '</h4>' +
        '                </div>';

    if (data.success) {
        modalHtml += '<div class="modal-body"><p>' + data.success + '</p></div>';
        modalHtml += '<div class="modal-footer">' +
            '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>' +
            '                </div>';
    } else {
        modalHtml += '<div class="modal-body">';
        for (var key in data) {
            if (data.hasOwnProperty(key)) {
                modalHtml += '<h3>' + key + '</h3>' +
                    '<ul>';
                data[key].forEach(function (error) {
                    modalHtml += '<li>' + error + '</li>';
                });
                modalHtml += '</ul>';
            }
        }
        modalHtml += '</div>';
        modalHtml += '<div class="modal-footer">' +
            '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>' +
            '                </div>';
    }

    modalHtml += '            </div><!-- /.modal-content -->' +
        '  </div><!-- /.modal-dialog -->' +
        '</div> <!-- /.modal -->';

    $('body').append(modalHtml);
    $('#' + random).modal('show');
}

