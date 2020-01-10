import hashlib
from time import time, sleep
import random

class TIMED_HMAC:
    # hmac proof = time|salt|hash256(time + hash256(salt + value sent))
    KEY_SOURCE = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!$*-"

    def GetTimeMSString():
        return str(round(time() * 1000)) # time() returns double of seconds

    def hmacFormat(time, salt, hexProof):
        return str(time) + "|" + str(salt) + "|" + str(hexProof)

    def calcHMAC(time, secret, value,salt=None, salt_length = 5):
        if not salt:
            salt = ''.join(
                random.choice(TIMED_HMAC.KEY_SOURCE) for i in range(salt_length)
            )
        proof = hashlib.sha256(
            (str(time) + hashlib.sha256((salt + secret + value).encode('ascii')).hexdigest())
            .encode('ascii')
            ).hexdigest()

        return TIMED_HMAC.hmacFormat(time, salt, proof);

    def verifyHMAC(hmac, secret, value, timespan_ms = 60000):
        result = False;
        reason = "init"

        try:
            splitValues = str(hmac).split('|')
            if len(splitValues) == 3:
                # Check token time:
                originalTime = float(splitValues[0])
                time_passed = round(time() * 1000) - originalTime;
                if time_passed >= 0 and time_passed < timespan_ms:
                    # Check if salt and pass are correct:
                    myHmac = TIMED_HMAC.calcHMAC(splitValues[0], secret, value, salt=splitValues[1]);
                    if myHmac == hmac:
                        result = True;
                    else:
                        reason = "Not eq. ex: " + myHmac + ", got:" + hmac
                else:
                    reason = "Token time passed, ex: " + str(timespan_ms) +", got: " + str(time_passed);
            else:
                reason = "Can't split into 3"
        except ex:
            reason = "Exception: " + str(ex);
            result = False;

        return (result, reason);


############################
####   Examples
############################


shared_secret = "xteuy1m1l6gi7my8dmht05xtifhj23"
value = "This is an example";

proof1 = TIMED_HMAC.calcHMAC(TIMED_HMAC.GetTimeMSString(), shared_secret , value);
print("Proof: '" + proof1 + "'")

(valid1, reason1) = TIMED_HMAC.verifyHMAC(proof1,shared_secret, value);
print("Is Valid? " + str(valid1) + ", Reason: " + reason1)
(valid2, reason2) = TIMED_HMAC.verifyHMAC(proof1,"bad_secret", value)
print("Is Valid wrong secret? " + str(valid2) + ", Reason: " + reason2)

token_time = 2; # 2 second
sleep(token_time + 1);

(valid3, reason3) = TIMED_HMAC.verifyHMAC(proof1,shared_secret, value, timespan_ms=token_time)
print("Is Valid after Time? " + str(valid3) + ", Reason: " + reason3)


