window.onload = function () {
    var ctx = document.getElementById('temperatureChart').getContext('2d');
    var temperatureChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [], // 시간을 나타내는 라벨
            datasets: [{
                label: 'Temperature',
                data: [],
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
                fill: false
            }, {
                label: 'Pressure',
                data: [],
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1,
                fill: false
            }]
        },
        options: {
            scales: {
                x: {
                    type: 'linear',
                    position: 'bottom'
                },
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    function updateChart(chart) {
        fetch('/api/data')
            .then(response => response.json())
            .then(data => {
                console.log(data); // 데이터를 콘솔에 출력하여 확인
                if (data.temperature && data.pressure) {
                    const currentTime = new Date().toLocaleTimeString();
                    // 데이터 추가
                    chart.data.labels.push(currentTime);
                    chart.data.datasets[0].data.push(data.temperature.reduce((a, b) => a + b) / data.temperature.length);
                    chart.data.datasets[1].data.push(data.pressure.reduce((a, b) => a + b) / data.pressure.length);
                    // 최대 10개의 데이터 포인트만 유지
                    if (chart.data.labels.length > 10) {
                        chart.data.labels.shift();
                        chart.data.datasets[0].data.shift();
                        chart.data.datasets[1].data.shift();
                    }
                    chart.update();
                } else {
                    console.error('Unexpected data structure:', data);
                }
            })
            .catch(error => console.error('Error fetching data:', error));
    }

    // 1초마다 차트 업데이트
    setInterval(() => {
        updateChart(temperatureChart);
    }, 1000);
};
