window.initIndexTable = function (options) {

    const table = new Tabulator(options.selector, {
        locale: "en-us",

        data: options.data,
        layout: "fitColumns",
        responsiveLayout: "collapse",

        pagination: true,
        paginationMode: "local",
        paginationSize: options.pageSize || 10,
        paginationSizeSelector: [25, 50, 100],

        placeholder: options.placeholder || "No records found",

        rowFormatter: function (row) {

            const data = row.getData();
            const idField = options.idField || "id" || "Id";

            row.getElement().addEventListener("click", () => {

                if (typeof options.onRowClick === "function") {
                    options.onRowClick(data, row);
                    return;
                }

                if (options.editUrl) {

                    const id = data[idField];
                    if (id !== undefined && id !== null) {
                        window.location.href = `${options.editUrl}/${id}`;
                    }
                }
            });
        },

        columns: options.columns
    });

    if (options.searchInput) {
        document.querySelector(options.searchInput)
            .addEventListener("input", function () {

                const value = this.value.toLowerCase();

                table.setFilter(data =>
                    Object.values(data)
                        .some(v =>
                            String(v ?? "")
                                .toLowerCase()
                                .includes(value)
                        )
                );
            });
    }

    return table;
};
