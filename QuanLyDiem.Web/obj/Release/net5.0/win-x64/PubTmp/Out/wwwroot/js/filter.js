const filterElements = document.querySelectorAll('.filter i');

let filterElementsArr = Array.from(filterElements);

// console.log(filterElementsArr);
// filterElementsArr.map(fe => fe.addEventListener('click', e => fe.classList.toggle('rotate')))
filterElementsArr.map(fe => fe.addEventListener('click', e => e.target.classList.toggle("rotate")))

