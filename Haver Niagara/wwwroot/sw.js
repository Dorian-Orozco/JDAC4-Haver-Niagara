
////Listeners for Push
////This ia a basic setup for just a title, message and icon
self.addEventListener('push', function (event) {

    var data = {};
    if (event.data) {
        data = event.data.json();
    }

    console.log('Notification Received:');
    console.log(data);

    var title = data.title;
    var message = data.message;
    var icon = "images/webPushIcon.png"; //

    event.waitUntil(self.registration.showNotification(title, {
        body: message,
        icon: icon,
        badge: icon
    }));
});

self.addEventListener('notificationclick', function (event) {
    event.notification.close();
});
//End of Push