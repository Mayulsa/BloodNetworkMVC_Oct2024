const bloodInventory = {
    init: function () {
        this.setupExpirationAlerts();
        this.setupFilterHandling();
        this.setupAutoRefresh();
    },

    setupExpirationAlerts: function () {
        // Auto-refresh expiration alerts every 5 minutes
        setInterval(() => {
            this.refreshExpirationAlerts();
        }, 300000);
    },

    refreshExpirationAlerts: function () {
        $.get('/BloodInventory/ExpirationAlerts', function (data) {
            $('#expirationAlerts').html(data);
        });
    },

    setupFilterHandling: function () {
        // Dynamic drawer loading based on freezer selection
        $('#Filter_FreezerId').change(function () {
            const freezerId = $(this).val();
            if (freezerId) {
                $.get(`/BloodInventory/GetDrawers/${freezerId}`, function (data) {
                    $('#Filter_DrawerId').html(data);
                });
            }
        });

        // Apply filters using AJAX
        $('#filterForm').submit(function (e) {
            e.preventDefault();
            const formData = $(this).serialize();
            $.get('/BloodInventory/FilteredUnits', formData, function (data) {
                $('#inventoryTable').html(data);
            });
        });
    },

    setupAutoRefresh: function () {
        // Auto-refresh inventory data every 10 minutes
        setInterval(() => {
            const formData = $('#filterForm').serialize();
            $.get('/BloodInventory/FilteredUnits', formData, function (data) {
                $('#inventoryTable').html(data);
            });
        }, 600000);
    }
};

$(document).ready(function () {
    bloodInventory.init();
});