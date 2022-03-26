

$(".btnUpdateShoppingCart").on('click', function (e) {
    e.preventDefault();
    Swal.fire({
        title: 'Are you sure?',
        text: "Do you want to Update quantity of Product in your shopping cart?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Update in my shopping cart!'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                'added!',
                'Your file has been added.',
                'success'
            )
            $.ajax({
                url: "/ShoppingCart/UpdateShoppingCart",
                data: { id: $(this).data('id'), quantity: $('#quantity').val() },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        Swal.fire(
                            'Added!',
                            'Your item has been Updated in your shopping cart!',
                            'success',
                        )
                        window.location.reload();
                    }
                }
            });
        }
    })

});


$(".btnAddShoppingCart").on('click', function (e) {
    e.preventDefault();
    Swal.fire({
        title: 'Are you sure?',
        text: "Do you want to add Product in your shopping cart?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Add in my shopping cart!'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                'added!',
                'Your file has been added.',
                'success'
            )
            $.ajax({
                url: "/ShoppingCart/AddShoppingCart",
                data: { id: $(this).data('id'), quantity: $(this).data('quantity'), detailquantity: $('#detailquantity').val() },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        Swal.fire(
                            'Added!',
                            'Your item has been Added in your shopping cart!',
                            'success',
                        )
                        window.location.reload();
                    }
                }
            });
        }
    })
    
});

$(".btnDeleteShoppingCart").on('click', function (e) {
    e.preventDefault();
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                'Deleted!',
                'Your item has been deleted in your shopping cart.',
                'success'
            )
            $.ajax({
                url: "/ShoppingCart/DeleteShoppingCart",
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.reload();
                    }
                }
            });
        }
    })
    
});

