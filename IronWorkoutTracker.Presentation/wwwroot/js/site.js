// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function() {
    // Handle modal triggers
    document.addEventListener('click', function(e) {
        const modalTrigger = e.target.closest('[data-bs-target="#programDayModal"]');
        
        if (modalTrigger) {
            e.preventDefault();
            
            const controller = modalTrigger.dataset.controller;
            const action = modalTrigger.dataset.action;
            const id = modalTrigger.dataset.id;
            const programId = modalTrigger.dataset.programId;
            
            let url = `/${controller}/${action}`;
            
            if (id) {
                url += `/${id}`;
            } else if (programId) {
                url += `?workoutProgramId=${programId}`;
            }
            
            console.log('Loading URL:', url);
            
            fetch(url)
                .then(response => {
                    if (!response.ok) throw new Error('Network response was not ok');
                    return response.text();
                })
                .then(html => {
                    document.getElementById('modalContent').innerHTML = html;
                    const modal = new bootstrap.Modal(document.getElementById('programDayModal'));
                    modal.show();
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert('Error loading form');
                });
        }
    });
});
