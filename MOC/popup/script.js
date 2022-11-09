const btnOpenModal = document.getElementById("open-modal");
const btnCloseModal = document.getElementById("close-modal");
const modalElm = document.getElementById("modal");

btnOpenModal.addEventListener("click", function () {
  modalElm.classList.add("open");
});

btnCloseModal.addEventListener("click", function () {
  modalElm.classList.remove("open");
});
