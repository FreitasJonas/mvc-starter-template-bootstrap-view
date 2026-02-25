(function () {

    const loader = document.getElementById("globalLoader");

    function showLoader() {
        loader.classList.add("active");
    }

    function hideLoader() {
        loader.classList.remove("active");
    }

    document.addEventListener("click", function (e) {

        const link = e.target.closest("a");

        if (!link) return;

        // ignora anchors e botões bootstrap
        if (
            link.getAttribute("href")?.startsWith("#") ||
            link.hasAttribute("data-bs-toggle") ||
            link.hasAttribute("data-no-loader")
        ) return;

        showLoader();
    });

    document.addEventListener("submit", function (e) {

        const form = e.target;

        if (form.hasAttribute("data-no-loader")) return;

        showLoader();
    });

    window.addEventListener("pageshow", function () {
        hideLoader();
    });

    hideLoader();

})();