document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const wygranyCheckbox = document.querySelector("#Zaklad_Wygrany");
    const rozliczonyCheckbox = document.querySelector("#Zaklad_Rozliczony");

    if (!form || !wygranyCheckbox || !rozliczonyCheckbox) return;

    rozliczonyCheckbox.addEventListener("change", function () {
        if (!this.checked) {
            wygranyCheckbox.checked = false;
            wygranyCheckbox.disabled = true;
            wygranyLabel.classList.add("disabled-checkbox");
        } else {
            wygranyCheckbox.disabled = false;
            wygranyLabel.classList.remove("disabled-checkbox");
        }
    });

    form.addEventListener("submit", function (e) {
        if (!rozliczonyCheckbox.checked && wygranyCheckbox.checked) {
            e.preventDefault();
            alert("Nie można zaznaczyć zakładu jako wygranego, jeśli nie jest rozliczony.");
        }
    });
});
