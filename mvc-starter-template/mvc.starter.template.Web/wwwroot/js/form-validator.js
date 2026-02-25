document.addEventListener("DOMContentLoaded", () => {

    document.querySelectorAll("form").forEach(form => {

        const inputs = form.querySelectorAll("input, textarea, select");

        function getErrorSpan(input) {
            const name = input.getAttribute("name");
            return form.querySelector(`[data-valmsg-for='${name}']`);
        }

        function showError(input, message) {
            const span = getErrorSpan(input);
            if (!span) return;

            span.textContent = message;
            span.classList.remove("field-validation-valid");
            span.classList.add("field-validation-error");
        }

        function clearError(input) {
            const span = getErrorSpan(input);
            if (!span) return;

            span.textContent = "";
            span.classList.remove("field-validation-error");
            span.classList.add("field-validation-valid");
        }

        function applyMask(input) {
            const mask = input.dataset.mask;
            if (!mask || mask.length === 0)
                return;

            input.addEventListener("input", () => {
                let v = input.value.replace(/\D/g, "");
                let i = 0;

                input.value = mask.replace(/0/g, () => v[i++] || "");
            });
        }

        function validate(input) {
            const value = input.value.trim();
            const msg = input.dataset.msg || "Campo inválido";

            if (input.dataset.required === "true" && !value) {
                showError(input, msg);
                return false;
            }

            if (input.dataset.min && value && value.length < parseInt(input.dataset.min)) {
                showError(input, msg);
                return false;
            }

            if (input.dataset.max && value && value.length > parseInt(input.dataset.max)) {
                showError(input, msg);
                return false;
            }

            // Regex só valida se houver valor
            console.log("Valor: " + value);
            if (input.dataset.regex && value) {                
                const regex = new RegExp(input.dataset.regex);
                if (!regex.test(value)) {
                    showError(input, msg);
                    return false;
                }
            }

            clearError(input);
            return true;
        }

        // Inicializar máscaras e validação on-blur
        inputs.forEach(input => {
            applyMask(input);
            input.addEventListener("blur", () => validate(input));
        });

        // Validação no submit
        form.addEventListener("submit", e => {
            let valid = true;

            inputs.forEach(input => {
                if (!validate(input))
                    valid = false;
            });

            if (!valid) {
                e.preventDefault();

                if (window.SmartAdminLoading)
                    SmartAdminLoading.hide();

                return;
            }
        });

    });
});

document.addEventListener("DOMContentLoaded", () => {
    if (window.SmartAdminLoading)
        SmartAdminLoading.hide();
});

