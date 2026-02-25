document.addEventListener("DOMContentLoaded", () => {

    const checkboxes = document.querySelectorAll(".permission-checkbox");

    /* ===============================
       ROOT PROTECTION
    =============================== */

    document.querySelectorAll(".permission-node[data-root='True'] .permission-checkbox")
        .forEach(cb => {
            cb.checked = true;
            cb.indeterminate = false;
            cb.disabled = true;
        });

    /* ===============================
       CHECKBOX LOGIC
    =============================== */

    checkboxes.forEach(cb => {

        cb.addEventListener("change", function () {

            const node = this.closest(".permission-node");
            const isRoot = node.dataset.root === "True";

            if (isRoot) {
                this.checked = true;
                this.indeterminate = false;
                return;
            }

            toggleChildren(node, this.checked);

            updateParents(node);
        });

    });

    function toggleChildren(node, checked) {

        const children = node.querySelectorAll(
            ":scope > .permission-children .permission-checkbox"
        );

        children.forEach(cb => {
            if (!cb.disabled) {
                cb.checked = checked;
                cb.indeterminate = false;
            }
        });
    }

    function updateParents(node) {

        const parentNode = node.parentElement.closest(".permission-node");
        if (!parentNode) return;

        const parentCheckbox = parentNode.querySelector(
            ":scope > .permission-header .permission-checkbox"
        );

        const childCheckboxes = parentNode.querySelectorAll(
            ":scope > .permission-children > .permission-node > .permission-header .permission-checkbox"
        );

        const total = childCheckboxes.length;
        const checkedCount = [...childCheckboxes].filter(cb => cb.checked).length;

        if (checkedCount === 0) {
            parentCheckbox.checked = false;
            parentCheckbox.indeterminate = false;
        }
        else if (checkedCount === total) {
            parentCheckbox.checked = true;
            parentCheckbox.indeterminate = false;
        }
        else {
            parentCheckbox.checked = false;
            parentCheckbox.indeterminate = true;
        }

        updateParents(parentNode);
    }

    /* ===============================
       FORCE PARENTS ON LOAD
       (importante ao editar)
    =============================== */

    checkboxes.forEach(cb => {
        if (cb.checked) {
            const node = cb.closest(".permission-node");
            updateParents(node);
        }
    });

    /* ===============================
       TOGGLE COLLAPSE
    =============================== */

    document.addEventListener("click", function (e) {

        const toggleBtn = e.target.closest(".toggle-node");
        if (!toggleBtn) return;

        const node = toggleBtn.closest(".permission-node");
        if (!node) return;

        const children = node.querySelector(":scope > .permission-children");
        if (!children) return;

        const icon = toggleBtn.querySelector("i");
        if (!icon) return;

        children.classList.toggle("d-none");

        if (children.classList.contains("d-none")) {
            icon.classList.remove("bi-chevron-down");
            icon.classList.add("bi-chevron-right");
        } else {
            icon.classList.remove("bi-chevron-right");
            icon.classList.add("bi-chevron-down");
        }
    });

});