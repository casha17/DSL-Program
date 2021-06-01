import {Client} from "../models/customers/Client"
import {Seat} from "../models/resources/Seat"
import {NightPlan} from "../models/schedules/NightPlan"
export type CreateCinemaBookingRequestModel = {
	name: string
	client: Client
	seat: Seat
	plan: NightPlan
} 
