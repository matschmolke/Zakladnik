document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const isWonCheckbox = document.querySelector("#IsWon");
    const isSettledCheckbox = document.querySelector("#IsSettled");

    if (!form || !isWonCheckbox || !isSettledCheckbox) return;

    isSettledCheckbox.addEventListener("change", function () {
        if (!this.checked) {
            isWonCheckbox.checked = false;
            isWonCheckbox.disabled = true;
            wygranyLabel.classList.add("disabled-checkbox");
        } else {
            isWonCheckbox.disabled = false;
            wygranyLabel.classList.remove("disabled-checkbox");
        }
    });

    form.addEventListener("submit", function (e) {
        if (!isSettledCheckbox.checked && isWonCheckbox.checked) {
            e.preventDefault();
            alert("Nie można zaznaczyć zakładu jako wygranego, jeśli nie jest rozliczony.");
        }
    });
});
