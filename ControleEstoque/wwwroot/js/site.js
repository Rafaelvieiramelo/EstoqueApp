//function initializeDataTable() {
//    $(document).ready(function () {
//        if ($.fn.DataTable.isDataTable('#produtossTable')) {
//            $('#produtossTable').DataTable().destroy();
//        }
//        $('#produtossTable').DataTable({
//            searching: true,
//            language: {
//                search: "Pesquisar:", // Altera o texto da barra de pesquisa
//                lengthMenu: "Mostrar _MENU_ registros por página",
//                zeroRecords: "Nenhum registro encontrado",
//                info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
//                infoEmpty: "Nenhum registro disponível",
//                infoFiltered: "(filtrado de _MAX_ registros no total)",
//                paginate: {
//                    first: "Primeiro",
//                    last: "Último",
//                    next: "Próximo",
//                    previous: "Anterior"
//                }
//            }
//        });
//    });
//}

window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}
