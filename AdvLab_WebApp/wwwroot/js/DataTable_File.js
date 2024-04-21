let table = $('#datatableData').DataTable();

table.on('click', 'tbody tr', function () {
    let data = table.row(this).data();

    alert('You clicked on ' + data[0] + "'s row");
});

new $.fn.dataTable.Buttons(table, {
    buttons: [
        'copy', 'excel', 'pdf'
    ]
});