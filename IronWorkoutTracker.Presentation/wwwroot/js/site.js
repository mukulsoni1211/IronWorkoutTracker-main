document.addEventListener('DOMContentLoaded', function() {
    // Handle modal triggers
    document.addEventListener('click', function(e) {
        // Check if clicked element has data-bs-target="#GlobalModal"
        const modalTrigger = e.target.closest('[data-bs-target="#GlobalModal"]');
        if (modalTrigger) {
            e.preventDefault();
            
            // Read data attributes from the clicked button
            const controller = modalTrigger.dataset.controller;
            const action = modalTrigger.dataset.action;
            const id = modalTrigger.dataset.id;
            const programId = modalTrigger.dataset.programId;
            
            let url = `/${controller}/${action}`;            
            const programDayId = modalTrigger.dataset.programDayId;
            const programDayExerciseId = modalTrigger.dataset.programDayExerciseId;

            if (id) {
                url += `/${id}`;
            } else if (programDayExerciseId) {
                url += `?programDayExerciseId=${programDayExerciseId}`;
            } else if (programDayId) {
                url += `?programDayId=${programDayId}`;
            } else if (programId) {
                url += `?workoutProgramId=${programId}`;
            }
  
            console.log('Loading URL:', url);
            console.log('Controller:', controller, 'Action:', action, 'ID:', id);
            debugger
            fetch(url)
                .then(response => {
                    if (!response.ok) throw new Error('Network response was not ok');
                    return response.text();
                })
                .then(html => {
                    document.getElementById('modalContent').innerHTML = html;
                    const modal = new bootstrap.Modal(document.getElementById('GlobalModal'));
                    modal.show();
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert('Error loading form');
                });
        }
    });
});
