$(document).ready(function () {
    $('#student-Table').DataTable({
        "responsive": true,
        "stateSave": true,
        "processing": true, // for show progress bar
        "language": {
            processing: '<div id="loading" style="margin: 25px;"><h5><img src="/img/Blocks-1s-200px.svg" width="50" height="50" /> &nbsp; Loading...</h5></div>'
        },
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        fixedColumns: {
            left: 2
        },
        scrollY: "350px",
        scrollX: true,
        scrollCollapse: false,
        "ordering": false,
        "ajax": {
            "url": "/Student/StudentDataTable",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [{
            "data": null,
            render: function (data, type, row, meta) {
                if (type === 'display') {
                    return '<div style="text-align: center;">' + (meta.row + meta.settings._iDisplayStart + 1) + '</div>';
                }
                return meta.row + meta.settings._iDisplayStart + 1;
            },

        }, {
            "data": "name",
            "name": "Name",
        }, {
            "data": "email",
            "name": "Email",
        }, {
            "data": "phoneNumber",
            "name": "PhoneNumber"
        }, {
            "data": null,
            "render": function (data, type, full) {
                //$("#showEditAction" + full.id).hide();
                //if (!canView) {
                //    $("#showViewAction" + full.id).hide();
                //}
                //$("#showDeleteAction" + full.id).hide();

                //return '<div class="dropdown">'
                //    + '<a class="btn jaoas-view-icon" href="#" role="button" id="dropdownMenuLink"'
                //    + ' data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">'
                //    + '<i class="fa-solid fa-ellipsis"></i>'
                //    + '</a>'
                //    + '<div class="dropdown-menu" id="actionBtn" aria-labelledby="dropdownMenuLink">'
                //    + '<a id="showEditAction' + full.id + '" class="dropdown-item" href="/SupervisingOrganization/UpdateSupervisingOrganization/' + full.id + '">Edit</a>'
                //    + '<a id="showDeleteAction' + full.id + '" class="dropdown-item" href="#" onclick="showDeleteModal(' + full.id + ')">Delete</a>'
                //    + '</div>'
                //    + '</div>';

                return `<div class="dropdown">
                      <a class="btn btn-secondary dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Dropdown link
                      </a>
                      <ul class="dropdown-menu">
                        <li><a class="dropdown-item" href="#">Edit</a></li>
                        <li><a class="dropdown-item" href="#">Delete</a></li>
                      </ul>
                    </div>`
            },
            className: "text-center",
        }
        ],
    });

});