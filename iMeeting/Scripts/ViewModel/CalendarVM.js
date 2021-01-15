//var ParticipantModel = function (participants) {
//    var self = this;
//    self.participants = ko.observableArray(participants);

//    self.addParticipant = function () {
//        self.participants.push({
//            ID_PARTICIPANT: '',
//            NOM: '',
//            PRENOM: '',
//            FONCTION: '',
//            EMAIL: '',
//            TELEPHONE: ''
//        });
//    };

//    self.removeParticipant = function (participant) {
//        self.participants.remove(participant);
//    };

//    self.save = function (form) {
//        alert("Could now transmit to server: " + ko.utils.stringifyJson(self.participants));
//        // To actually transmit to server as a regular form post, write this: ko.utils.postJson($("form")[0], self.participants);
//    };
//};

//var viewModel = new ParticipantModel([
//    { name: "Tall Hat", price: "39.95" },
//    { name: "Long Cloak", price: "120.00" }
//]);
//ko.applyBindings(viewModel);