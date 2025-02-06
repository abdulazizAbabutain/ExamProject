const fetchDataBtn = document.getElementById('fetchDataBtn');
const outputEl = document.getElementById('output');

fetchDataBtn.addEventListener('click', async () => {
  try {
    // Call your ASP.NET Core API on http://localhost:5000
    console.log('calling ');
    const response = await fetch('https://localhost:7072/api/Questions');
    const data = await response.json();
    outputEl.innerText = JSON.stringify(data, null, 2);
  } catch (error) {
    outputEl.innerText = 'Error: ' + error.message;
    console.error(error);
  }
});
