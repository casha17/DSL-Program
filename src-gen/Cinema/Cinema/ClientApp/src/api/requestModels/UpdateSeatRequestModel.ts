import {NightPlan} from "../models/schedules/NightPlan"
export type UpdateSeatRequestModel = {
	id: string
	name: string
	weight: number
	nightPlans: NightPlan[]
} 
